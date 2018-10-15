import React, { Component } from 'react';
import CreateProduct from './CreateProduct';
import AllProducts from './AllProducts';

class App extends Component {
  render() {
      return (
        <div className="App">
          <div className="navbar">
            <h2 className="center">Product Catalog</h2>
          </div>
          <CreateProduct />
          <AllProducts />
        </div>
      );
  }
  }
export default App;