import { Injectable } from "@angular/core";
import { SearchStoreDto } from "../ElarabyCA-api";

@Injectable({
    providedIn: 'root'
})
export class StoresValidations {
    validateDeleteStore(store: SearchStoreDto) {
        if (store.totalBalance > 0)
            return 'ServerMessages.Store.CannotDeleteStoreHasInventory';
        else
            return null;
    }
}
