import DogBreed from "../components/DogBreed";
import axios from "axios";

export class DogBreedAdapter {
  state = {
    isLoading: true,
    users: [],
    error: null
  };

  async getResults() {
    let result = [];
    await axios
      .all([
        axios.get("https://localhost:44368/api/predict/getResults?row=" + 0),
        axios.get("https://localhost:44368/api/predict/getResults?row=" + 1),
        axios.get("https://localhost:44368/api/predict/getResults?row=" + 2),
        axios.get("https://localhost:44368/api/predict/getResults?row=" + 3),
        axios.get("https://localhost:44368/api/predict/getResults?row=" + 4)
      ])
      .then(
        axios.spread(
          (response1, response2, response3, response4, response5) => {
            console.log("Resp1", response1.data);
            console.log("Resp2", response2.data);
            console.log("Resp3", response3.data);
            console.log("Resp4", response4.data);
            console.log("Resp5", response5.data);
            result[0] = response1.data;
            result[1] = response2.data;
            result[2] = response3.data;
            result[3] = response4.data;
            result[4] = response5.data;
          }
        )
      );

    return await result;
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
