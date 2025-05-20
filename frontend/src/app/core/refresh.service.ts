import { Injectable } from "@angular/core";
import { Observable, Subject } from "rxjs";

@Injectable({ providedIn: 'root' })
export class RefreshService {

    private _refresh$ = new Subject<void>();

    get refreshNeeded$(): Observable<void> {
        return this._refresh$.asObservable();
    }

    notifyRefresh(): void {
        this._refresh$.next();
    }
    
}