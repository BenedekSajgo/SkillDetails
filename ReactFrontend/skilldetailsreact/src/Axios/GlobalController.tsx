import Axios from "axios";
import { Service } from "./Service";

export const GlobalController = {
  get,
  remove,
};

function get<T>(path: string, params?: any) {
  return new Promise<Service<T>>((resolve, reject) => {
    Axios.get(path, params)
      .then((response) => {
        if (response.status >= 400) return Promise.reject(response);
        return response.data;
      })
      .then((response) => {
        resolve({ status: "loaded", payload: response.data });
      })
      .catch((error) => {
        reject({ status: "error", error });
      });
  });
}

function remove<T>(path: string, params?: any) {
  return new Promise<Service<T>>((resolve, reject) => {
    Axios.delete(path, params)
      .then((response) => {
        if (response.status >= 400) return Promise.reject(response);
        return response.data;
      })
      .then((response) => {
        resolve({ status: "loaded", payload: response.data });
      })
      .catch((error) => {
        reject({ status: "error", error });
      });
  });
}
