import { Contact } from "./contact";

export type User = {
  id: number;
  firstName: string;
  lastName: string;
  username: string;
  passwordHash: Uint8Array;
  passwordSalt: Uint8Array;
  contacts: Contact[];
};
