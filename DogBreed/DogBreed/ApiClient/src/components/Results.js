import React from "react";
import { observer, inject } from "mobx-react";

@inject("rootStore")
@observer
class Results extends React.Component {
  render() {
    const { rootStore } = this.props;
    return (
      <React.Fragment>
        {rootStore.dogBreedStore.dogBreedResults.data !== undefined ? (
          <h1>
            {rootStore.dogBreedStore.dogBreedResults.data.map(item => (
              <p>{item.name}</p>
            ))}
          </h1>
        ) : null}
      </React.Fragment>
    );
  }
}

export default Results;
