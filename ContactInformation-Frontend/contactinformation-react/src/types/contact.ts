import { Address } from "./address";
import { User } from "./user";

export type Contact = {
  id: number;
  firtName: string;
  lastName: string;
  addresses: Address[];
  phoneNumber: string;
  emailAddress: string;
  birthDate: Date;
  favorite: boolean;
  user: User;
  userId: number;
};
