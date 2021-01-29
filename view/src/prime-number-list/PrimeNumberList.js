import './PrimeNumberList.css';

import React from 'react'

import API from '../api.js'

class PrimeNumberList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      maximumPrimeValue: null,
      primes: [],
      numberOfPages: 1,
      pageNumber: 1,

      errorMessage: null
    };

    this.maximumPrimeValueChanged = this.maximumPrimeValueChanged.bind(this);
    this.getPrimeNumbers = this.getPrimeNumbers.bind(this);
    this.pageNumberChanged = this.pageNumberChanged.bind(this);
  }

  maximumPrimeValueChanged(event) {
    this.setState({maximumPrimeValue: event.target.value});
  }

  getPrimeNumbers() {
    API.get(`/primenumber/${this.state.maximumPrimeValue}/20/${this.state.pageNumber - 1}`)
      .then(response => {
        this.setState({
          primes: response.data.result.primeNumbers,
          numberOfPages: response.data.result.numberOfPages
        })
      })
      .catch(error => {
        var errors = error.response.data?.errors;
        if (errors?.max[0] !== "") {
          this.setState({errorMessage: error.response.data?.errors?.max[0]});
        }
        console.log(error.response.data)
      });
  }

  pageNumberChanged(event) {
    this.setState({pageNumber: parseInt(event.target.value)}, () => {
      this.getPrimeNumbers();
    });
  }

  range(start, end) {
    var array = [];
    for(let i = start; i <= end; i++) {
      array.push(i);
    }
    return array;
  }

  render() {
    return (
      <div>
        <h2>Prime Numbers</h2>

        {this.state.errorMessage &&
          <p>{this.state.errorMessage}</p>
        }

        <label htmlFor="maximumPrimeValue">Maximum Prime Value </label>
        <input id="maximumPrimeValue" name="maximumPrimeValue" onChange={this.maximumPrimeValueChanged}></input>
        <button onClick={this.getPrimeNumbers}>Get Primes</button>

        <hr/>

        {this.state.numberOfPages > 1 &&
          <div>
            <label htmlFor="pageNumber">Page </label>
            <select id="pageNumber" value={this.state.pageNumber} onChange={this.pageNumberChanged}>
              {this.range(1, this.state.numberOfPages).map(index => <option value={index} key={index.toString()} readOnly={true}>{index}</option>)}
            </select>
            <hr/>
          </div>
        }

        {this.state.primes.length > 0 &&
          <ul>
            {(this.state.pageNumber !== 1) && <p>...</p>}
            {this.state.primes.map(prime => <li key={prime.toString()}>{prime}</li>)}
            {(this.state.numberOfPages !== this.state.pageNumber) && <p>...</p>}
          </ul>
        }
      </div>
    )
  }
}

export default PrimeNumberList;
