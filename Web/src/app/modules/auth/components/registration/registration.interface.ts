

import { FileInfo } from "@progress/kendo-angular-upload";


interface IRegistrationForm1 {
    //step1
    Email: string;
    Password: string;
    ConfirmPassword: string;
}
interface IRegistrationForm {
    //step1
    Email: string;
    Password: string;
    ConfirmPassword: string;
    //step2
    firstName: string;
    lastName: string;
    phoneNumber: string;
    lineAccountId: string;
    

}
const initss: IRegistrationForm1 = {
    //step1
    Email: '',
    Password: '',
    ConfirmPassword: '',
}
const inits: IRegistrationForm = {
    //step1
    Email: '',
    Password: '',
    ConfirmPassword: '',
    //step2
    
    firstName: '',
    lastName: '',
    phoneNumber: '',
    lineAccountId: '',
    




};

export { IRegistrationForm, inits };
export { IRegistrationForm1, initss };
