using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using OneFantasy.Api.DTOs;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OneFantasy.Api.Domain.Filters
{
    public class PolymorphicResponseOperationFilter : IOperationFilter
    {
        private static readonly (Type BaseType, string Discriminator, string[] ConcreteSchemas)[] _maps =
        [
            (
                typeof(IParticipationDtoResponse),
                "participationType",
                new[]
                {
                    "ParticipationStandardDtoResponse",
                    "ParticipationExtraDtoResponse",
                    "ParticipationSpecialDtoResponse"
                }
            ),
            (
                typeof(IMinigameDtoResponse),
                "gameType",
                new[]
                {
                    "MinigameResultDtoResponse",
                    "MinigameMatchDtoResponse",
                    "MinigameScoresDtoResponse",
                    "MinigamePlayersDtoResponse"
                }
            )
        ];

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (!operation.Responses.TryGetValue("200", out var resp) ||
                !resp.Content.TryGetValue("application/json", out var mediaType))
                return;

            var apiResp = context.ApiDescription.SupportedResponseTypes
                .FirstOrDefault(r => r.StatusCode == 200);
            if (apiResp == null)
                return;

            var returnType = apiResp.Type;
            foreach (var (baseType, disc, schemas) in _maps)
            {
                if (baseType.IsAssignableFrom(returnType))
                {
                    mediaType.Schema = CreateOneOfSchema(schemas, disc);
                    return;
                }

                if (typeof(IEnumerable).IsAssignableFrom(returnType)
                    && returnType.IsGenericType
                    && baseType.IsAssignableFrom(returnType.GetGenericArguments()[0]))
                {
                    mediaType.Schema = new OpenApiSchema
                    {
                        Type = "array",
                        Items = CreateOneOfSchema(schemas, disc)
                    };
                    return;
                }
            }
        }

        private static OpenApiSchema CreateOneOfSchema(string[] schemaIds, string disc) => new()
        {
            OneOf = [.. schemaIds
                .Select(id => new OpenApiSchema
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.Schema,
                        Id = id
                    }
                })
            ],
            Discriminator = new OpenApiDiscriminator
            {
                PropertyName = disc,
                Mapping = schemaIds.ToDictionary(
                        id => char.ToLowerInvariant(id[0]) + id[1..],
                    id => $"#/components/schemas/{id}"
                )
            }
        };
    }
}
