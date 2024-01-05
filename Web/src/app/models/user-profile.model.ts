import { AccessTokenModel } from './access-token.model';

export class UserProfileModel extends AccessTokenModel {
  [_:string] : any;
  id: string;
  username: string;
  firstname: string;
  lastname: string;
  phone : string;
  addressDescription : string;
  accountId : string;
  roles : string;
  roomId : string;
  idCard : string;
  password : string;
  confirmPassword : string;

  constructor() {
    super();
    this.id = '';
    this.username =  '';
    this.firstname = '';
    this.lastname = '';
    this.phone = '';
    this.addressDescription =  '';
    this.accountId =  '';
    this.roles = '';
    this.roomId = '';
    this.idCard = '';
    this.password = '';
    this.confirmPassword = '';
  }
}
