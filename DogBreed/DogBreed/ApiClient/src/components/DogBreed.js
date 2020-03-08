import React from "react";
import axios from "axios";
import ImageUploader from "react-images-upload";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "./../DogBreed.css";
import ClipLoader from "react-spinners/ClipLoader";
import { css } from "@emotion/core";
import Spinner from "react-bootstrap/Spinner";

const override = css`
  display: block;
  margin: 0 auto;
  border-color: white;
`;

let dogBreedData = null;
const data = "background-color: #282c34";

class DogBreed extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      picture: null,
      loading: false
    };
  }
  render() {
    return (
      <React.Fragment>
        <div className="App">
          <ToastContainer />
          <ImageUploader
            className="ImgUpload"
            withIcon={true}
            buttonText="Choose image"
            onChange={this.onDrop}
            imgExtension={[".jpg", ".png"]}
            maxFileSize={5242880}
            withPreview={true}
            singleImage={true}
            fileContainerStyle={{ backgroundColor: "#282c34" }}
          />
          <button
            className="BtnUpload"
            variant="success"
            onClick={this.uploadHandler}
            disabled={!this.state.picture}
            size="sm"
          >
            Classify dog breed
          </button>
          <div className="sweet-loading">
            <Spinner animation="border" variant="primary" />
            <ClipLoader
              css={override}
              size={40}
              color={"#123abc"}
              loading={this.state.loading}
            />
          </div>

          {(dogBreedData !== null) & (dogBreedData !== undefined)
            ? dogBreedData.data.map((item, index) => (
                <table className="resultTable">
                  <tr>
                    <th>Dog breed</th>
                    <th>Score</th>
                  </tr>
                  <tr>
                    <td>{item.name}</td>
                    <td>{item.score.toFixed(4) * 100} %</td>
                  </tr>
                </table>
              ))
            : null}
        </div>
      </React.Fragment>
    );
  }

  onDrop = event => {
    this.setState({
      picture: event[0]
    });
  };

  isLoading = set => {
    this.setState({
      loading: set
    });
  };

  notify = () => toast("You need to upload image!");

  uploadHandler = () => {
    if (this.dogBreedData === null || this.dogBreedData === undefined) {
      this.notify();
    }
    const formData = new FormData();
    formData.append("formData", this.state.picture, this.state.picture.name);
    this.isLoading(true);
    axios
      .post("https://localhost:5001/api/predict/classify", formData)
      .then(res => {
        dogBreedData = res;
        console.log("resp", dogBreedData);
        this.isLoading(false);
      });
  };
}

export default DogBreed;
