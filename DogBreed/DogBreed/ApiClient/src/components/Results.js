import React from "react";
import { observer, inject } from "mobx-react";

@inject("rootStore")
@observer
class Results extends React.Component {
  render() {
    const { rootStore } = this.props;
    console.log("Reuslt klasa", rootStore.dogBreedStore.dogBreedResults);
    return (
      <React.Fragment>
        {rootStore.dogBreedStore.dogBreedResults !== undefined ? (
          <h1>
            {rootStore.dogBreedStore.dogBreedResults.map(item => (
              <React.Fragment>
                <li>
                  <img
                    alt="test"
                    src={`data:image/jpeg;base64,${item.file}`}
                  ></img>
                  {item.predictionResults.name}
                </li>
              </React.Fragment>
            ))}
          </h1>
        ) : null}
      </React.Fragment>
    );
  }
}

export default Results;
