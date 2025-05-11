using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using OneFantasy.Api.DTOs;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneFantasy.Api.Domain.Filters
{
    public class PolymorphicResponseOperationFilter : IOperationFilter
    {
        private static readonly (Type ResponseType, string Discriminator, string[] ConcreteSchemas, bool IsArray)[] _maps =
        [
            (typeof(IMinigameDtoResponse),
            "gameType",
            new[]
            {
                "MinigameResultDtoResponse",
                "MinigameMatchDtoResponse",
                "MinigameScoresDtoResponse",
                "MinigamePlayersDtoResponse"
            },
            false),
            (typeof(List<IMinigameDtoResponse>),
            "gameType",
            new[]
            {
                "MinigameResultDtoResponse",
                "MinigameMatchDtoResponse",
                "MinigameScoresDtoResponse",
                "MinigamePlayersDtoResponse"
            },
            true),
            (typeof(IParticipationDtoResponse),
            "participationType",
            new[]
            {
                "ParticipationStandartDtoResponse",
                "ParticipationExtraDtoResponse",
                "ParticipationSpecialDtoResponse"
            },
            false),
            (typeof(List<IParticipationDtoResponse>),
            "participationType",
            new[]
            {
                "ParticipationStandartDtoResponse",
                "ParticipationExtraDtoResponse",
                "ParticipationSpecialDtoResponse"
            },
            true)
        ];

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Responses.TryGetValue("200", out var response) ||
                !response.Content.TryGetValue("application/json", out var mediaType))
                return;

            var apiResp = context.ApiDescription.SupportedResponseTypes
                .FirstOrDefault(r => r.StatusCode == 200 &&
                    _maps.Any(m => m.ResponseType == r.Type));
            if (apiResp == null) return;

            var (ResponseType, Discriminator, ConcreteSchemas, IsArray) = _maps.First(m => m.ResponseType == apiResp.Type);

            mediaType.Schema = IsArray
                ? CreateArraySchema(ConcreteSchemas, Discriminator)
                : CreateObjectSchema(ConcreteSchemas, Discriminator);
        }

        private static OpenApiSchema CreateArraySchema(string[] ids, string disc) => new()
        {
            Type = "array",
            Items = CreateObjectSchema(ids, disc)
        };

        private static OpenApiSchema CreateObjectSchema(string[] ids, string disc) => new()
        {
            OneOf = [.. ids.Select(id => new OpenApiSchema
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.Schema,
                    Id = id
                }
            })],
            Discriminator = new OpenApiDiscriminator
            {
                PropertyName = disc,
                Mapping = ids.ToDictionary(
                    id => char.ToLowerInvariant(id[0]) + id[1..],
                    id => $"#/components/schemas/{id}"
                )
            }
        };

    }
}
