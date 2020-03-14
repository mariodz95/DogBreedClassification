import DogBreed from "../components/DogBreed";
import axios from "axios";

export class DogBreedAdapter {
  async getResults() {
    let test = [];
    // Promise is resolved and value is inside of the response const.
    await axios
      .get("https://localhost:44368/api/predict/getResults")
      .then(res => {
        console.log("Adapter1", res);

        console.log("Adapter", JSON.parse(res.data));
        test = res;
        return test.data;
      })
      .catch(error => console.log("Error:", error));
    return await test;
  }

  async getPrediction(formData) {
    let result = [];
    await axios
      .post("https://localhost:44368/api/predict/classify", formData)
      .then(res => {
        result = res;
      });
    return await result;
  }
}
