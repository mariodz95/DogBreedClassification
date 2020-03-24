import axios from "axios";
import { RootStore } from "../shared/stores/RootStore";

export class DogBreedAdapter {
  async getResults(data) {
    let result = [];
    let counter = data;

    await axios
      .all([
        axios
          .get("https://localhost:44368/api/predict/getResults?row=" + counter)
          .then(response1 => {
            result[0] = response1.data;
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1)
          )
          .then(response2 => {
            result[1] = response2.data;
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1)
          )
          .then(response2 => {
            result[2] = response2.data;
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1)
          )
          .then(response2 => {
            result[3] = response2.data;
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1)
          )
          .then(response2 => {
            result[4] = response2.data;
          })
      ])
      .catch(() => null);

    return await result;
  }

  async getPrediction(formData) {
    let result = [];
    await axios
      .post("https://localhost:44368/api/predict/registration", formData)
      .then(res => {
        result = res;
      });
    return await result;
  }

  async registration(email, password) {
    let result;
    await axios
      .post(
        "https://localhost:44368/api/account/registration?email=" +
          email +
          "&password=" +
          password
      )
      .then(res => {
        result = res;
      });

    return await result;
  }

  async login(email, password) {
    let result;
    await axios
      .post(
        "https://localhost:44368/api/account/login?email=" +
          email +
          "&password=" +
          password
      )
      .then(res => {
        result = res;
      });
    return await result;
  }
}
