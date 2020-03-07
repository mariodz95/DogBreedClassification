import React from "react";
import axios from "axios";
import Button from "react-bootstrap/Button";
import ImageUploader from "react-images-upload";

let dogBreed = null;

class DogBreed extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      picture: null
    };
  }

  render() {
    return (
      <React.Fragment>
        <ImageUploader
          withIcon={true}
          buttonText="Choose images"
          onChange={this.onDrop}
          imgExtension={[".jpg", ".png"]}
          maxFileSize={5242880}
          withPreview={true}
          singleImage={true}
        />
        <Button variant="success" onClick={this.uploadHandler}>
          Upload Image!
        </Button>
      </React.Fragment>
    );
  }

  onDrop = event => {
    this.setState({
      picture: event[0]
    });
  };

  uploadHandler = () => {
    const formData = new FormData();
    formData.append("formData", this.state.picture, this.state.picture.name);
    axios
      .post("https://localhost:5001/api/predict/classify", formData)
      .then(res => {
        console.log("resp", res.data);
        dogBreed = res;
      });
  };
}

export default DogBreed;
