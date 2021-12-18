import { Injectable } from "@angular/core";
import { SMSMessageDto } from "../ElarabyCA-api";

@Injectable({
    providedIn: 'root'
})
export class SMSMessageValidations {
    validateDeleteSMSMessage(smsMessage: SMSMessageDto) {
        // if (smsMessage.totalBalance > 0)
        //     return 'ServerMessages.SMSMessage.CannotDeleteSMSMessageHasInventory';
        // else
        //     return null;

        return true;
    }
}
