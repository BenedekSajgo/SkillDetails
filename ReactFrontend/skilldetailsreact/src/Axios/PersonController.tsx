import { urlPerson } from "../endpoints";
import { PersonDetailInterface } from "../Interfaces/PersonDetailInterface";
import { PersonSearchInterface } from "../Interfaces/PersonSearchInterface";
import { GlobalController } from "./GlobalController";

export const PersonController = {
  getPerson,
  getPersons,
  getPersonsIncludeDeleted,
  deletePerson,
  restorePerson,
};

function getPerson(id: number) {
  return GlobalController.get<PersonDetailInterface>(`${urlPerson}/${id}`);
}

function getPersons() {
  return GlobalController.get<PersonSearchInterface[]>(`${urlPerson}`);
}

function getPersonsIncludeDeleted() {
  return GlobalController.get<PersonSearchInterface[]>(`${urlPerson}/deleted`);
}

function deletePerson(id: number) {
  return GlobalController.remove<PersonSearchInterface>(`${urlPerson}/${id}`);
}

function restorePerson(id: number) {
  return GlobalController.remove<PersonSearchInterface>(
    `${urlPerson}/${id}/restore`
  );
}
