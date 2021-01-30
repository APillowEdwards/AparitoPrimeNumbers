import './PrimeNumberList.css';

import React from 'react'

import API from '../api.js'

class PrimeNumberList extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      maximumPrimeValue: "",
      pageSize: 25,
      primes: [],
      numberOfPages: 1,
      pageNumber: 1,

      errorMessage: null
    };

    this.formValueChanged = this.formValueChanged.bind(this);
    this.getPrimeNumbers = this.getPrimeNumbers.bind(this);
    this.pageNumberChanged = this.pageNumberChanged.bind(this);
  }

  formValueChanged(event) {
    this.setState({[event.target.name]: event.target.value});
  }

  getPrimeNumbers() {
    API.get(`/primenumber/${this.state.maximumPrimeValue}/${this.state.pageSize}/${this.state.pageNumber - 1}`)
      .then(response => {
        this.setState({
          primes: response.data.result.primeNumbers,
          numberOfPages: response.data.result.numberOfPages
        })
      })
      .catch(error => {
        var message = "There has been a problem with the application. Please try again later.";

        var errors = error?.response?.data?.errors;

        if (errors) {
          if (errors?.max[0] !== "") {
            message = error.response.data?.errors?.max[0];
          }
        }

        this.setState({errorMessage: message});
      });
  }

  pageNumberChanged(event) {
    this.changePageNumber(parseInt(event.target.value));
  }

  changePageNumber(newValue) {
    this.setState({pageNumber: newValue}, () => {
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
        <h1>Prime Numbers</h1>

        {this.state.errorMessage &&
          <div className="alert alert-secondary">{this.state.errorMessage}</div>
        }

        <label htmlFor="maximumPrimeValue">Maximum Prime Value</label>
        <input id="maximumPrimeValue" className="form-control mb-2" name="maximumPrimeValue" value={this.state.maximumPrimeValue} onChange={this.formValueChanged}></input>

        <label htmlFor="pageSize">Page Size</label>
        <select id="pageSize" className="form-control mb-2" name="pageSize" value={this.state.pageSize} onChange={this.formValueChanged}>
          <option value="25">25</option>
          <option value="50">50</option>
          <option value="100">100</option>
        </select>

        <button className="btn btn-primary" onClick={this.getPrimeNumbers}>Get Primes</button>

        <hr/>

        {this.state.numberOfPages > 1 &&
          <div>
            <label htmlFor="pageNumber">Page</label>
            <select id="pageNumber" className="form-control" value={this.state.pageNumber} onChange={this.pageNumberChanged}>
              {this.range(1, this.state.numberOfPages).map(index => <option value={index} key={index.toString()} readOnly={true}>{index}</option>)}
            </select>

            <hr/>
          </div>
        }

        {this.state.primes.length > 0 &&
          <div>
            <PrimeNumberRow clickable={true} invisible={this.state.pageNumber === 1} value={<i className="icofont-arrow-up"></i>} onClick={() => this.changePageNumber(this.state.pageNumber - 1)} />

            {this.state.primes.map(prime => <PrimeNumberRow key={prime.toString()} value={prime} />)}

            <PrimeNumberRow clickable={true} invisible={this.state.numberOfPages === this.state.pageNumber} value={<i className="icofont-arrow-down"></i>} onClick={() => this.changePageNumber(this.state.pageNumber + 1)} />
          </div>
        }
      </div>
    )
  }
}

function PrimeNumberRow(props) {
  return <div className={"prime-number-row " + (props.invisible ? "invisible " : " ") + (props.clickable ? "clickable" : "")} onClick={props.onClick}>{props.value}</div>
}

export default PrimeNumberList;
