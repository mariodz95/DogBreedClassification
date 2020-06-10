import axios from "axios";

export class DogBreedAdapter {
  async getResults(data, userId) {
    let result = [];
    let counter = data - 1;
    await axios
      .all([
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1) +
              "&userId=" +
              userId
          )
          .then((response) => {
            result.push(response.data);
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1) +
              "&userId=" +
              userId
          )
          .then((response) => {
            result.push(response.data);
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1) +
              "&userId=" +
              userId
          )
          .then((response) => {
            result.push(response.data);
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1) +
              "&userId=" +
              userId
          )
          .then((response) => {
            result.push(response.data);
          }),
        axios
          .get(
            "https://localhost:44368/api/predict/getResults?row=" +
              (counter = counter + 1) +
              "&userId=" +
              userId
          )
          .then((response) => {
            result.push(response.data);
          }),
      ])
      .catch(() => null);
    return result;
  }

  async getPrediction(formData, userId) {
    let result = [];
    await axios
      .post(
        "https://localhost:44368/api/predict/classify?userId=" +
          userId +
          "&formData",
        formData
      )
      .then((res) => {
        result = res;
      });
    return result;
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
      .then((res) => {
        result = res;
      });

    return result;
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
      .then((res) => {
        result = res;
      });
    return result;
  }
}
