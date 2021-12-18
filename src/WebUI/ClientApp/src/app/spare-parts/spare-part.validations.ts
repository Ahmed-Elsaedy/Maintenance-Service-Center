import { SearchSparePartDto } from "../ElarabyCA-api";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: 'root'
})
export class SparePartValidations {
    validateIfCanDeleteSparePart(sparePart: SearchSparePartDto) {
        if (sparePart.totalBalance > 0)
            return 'ServerMessages.SparePart.CannotDeleteSparePartHasInventory';
        else
            return null;
    }
}
