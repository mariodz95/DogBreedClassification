import DogBreed from "../components/DogBreed";
import axios from "axios";

export class DogBreedAdapter {
  async getResults() {
    let test = [];
    // Promise is resolved and value is inside of the response const.
    await axios
      .get("https://localhost:44368/api/predict/getResults", {
        headers: {
          "Content-Type": "application/json"
        },
        transformResponse: axios.defaults.transformResponse.concat(data => {
          console.log("transform data", data); // this should now be JSON
        })
      })
      .then(res => {
        console.log("Adapter1", res);
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
