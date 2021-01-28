import './PrimeNumberList.css';

import React from 'react'

import API from '../api.js'

class PrimeNumberList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      maximumPrimeValue: null,
      primes: []
    };

    this.maximumPrimeValueChanged = this.maximumPrimeValueChanged.bind(this);
    this.getPrimeNumbers = this.getPrimeNumbers.bind(this);
  }

  maximumPrimeValueChanged(event) {
    this.setState({maximumPrimeValue: event.target.value});
  }

  getPrimeNumbers(event) {
    API.get(`/primenumber/${this.state.maximumPrimeValue}`)
      .then(response => {
        this.setState({primes: response.data.result})
      })
      .catch(error => {

      });
  }

  render() {
    return (
      <div>
        <label htmlFor="maximumPrimeValue">Maximum Prime Value</label>
        <input id="maximumPrimeValue" name="maximumPrimeValue" onChange={this.maximumPrimeValueChanged}></input>
        <button onClick={this.getPrimeNumbers}>Get Primes</button>

        <ul>
          {this.state.primes.map(prime => <li key={prime.toString()}>{prime}</li>)}
        </ul>
      </div>
    )
  }
}

export default PrimeNumberList;
