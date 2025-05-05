import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailAuthDialogComponent } from './email-auth-dialog.component';

describe('EmailAuthDialogComponent', () => {
  let component: EmailAuthDialogComponent;
  let fixture: ComponentFixture<EmailAuthDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmailAuthDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EmailAuthDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
