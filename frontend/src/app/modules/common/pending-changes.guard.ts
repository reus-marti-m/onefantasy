// pending-changes.guard.ts
import { Injectable } from '@angular/core';
import {
    CanDeactivate,
    ActivatedRouteSnapshot,
    RouterStateSnapshot,
} from '@angular/router';
import { Observable } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';

export interface ComponentCanDeactivate {
    hasChanges: boolean;
}

@Injectable({ providedIn: 'root' })
export class PendingChangesGuard implements CanDeactivate<ComponentCanDeactivate> {
    constructor(private dialog: MatDialog) { }

    canDeactivate(
        component: ComponentCanDeactivate
    ): Observable<boolean> | boolean {
        if (!component.hasChanges) {
            return true;
        }
        const dialogRef = this.dialog.open(ConfirmDialogComponent, {
            data: {
                title: 'Canvis sense desar',
                message: 'Tens canvis pendents. ¿Segur que vols sortir sense desar?'
            },
            disableClose: true
        });
        return dialogRef.afterClosed();
    }
}
