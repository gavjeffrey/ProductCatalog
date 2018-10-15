import React, { Component } from 'react';
import { connect } from 'react-redux';
import Product from './Product';
import EditProduct from './EditProduct';
import Workbook from 'react-excel-workbook'
import {
  FETCH_PRODUCTS_BEGIN,
  FETCH_PRODUCTS_SUCCESS,
  FETCH_PRODUCTS_FAILURE
} from './ProductActions';

class AllProducts extends Component {

  componentDidMount() {
    this.props.dispatch({ type: FETCH_PRODUCTS_BEGIN });
    fetch("http://localhost:30649/api/product")
    .then(this.handleErrors)
    .then(res => res.json())
    .then(result => {
      this.props.dispatch({ type: FETCH_PRODUCTS_SUCCESS, payload: result });
    })
    .catch(error => {
      this.props.dispatch({ type: FETCH_PRODUCTS_FAILURE, payload: error });
    });
  }
  
  handleErrors(response) {
    if (!response.ok) {
      throw Error(response.statusText);
    }
    return response;
  }

  render() {
    const { error, loading, products } = this.props;
    
    if (error) {
      return <div>Error! {error.message}</div>;
    }
    
    if (loading) {
      return <div>Loading...</div>;
    }

    let productsRender;
    if (products.length > 0) {
      productsRender = 
      <div>
          <h1 className="product_heading">All Products</h1>
          {
            this.props.products.map((product) => (
              <div key={product.id}>
                {product.editing ? <EditProduct product={product} key={product.id} /> :
                <Product key={product.id} product={product} />}
              </div>
          ))
          }
          <div className="product">
            <Workbook filename="products.xlsx" element={<button className="openExcel">Open in Excel</button>}>
              <Workbook.Sheet data={this.props.products} name="Products">
                <Workbook.Column label="Name" value="name"/>
                <Workbook.Column label="Price" value="price"/>
                <Workbook.Column label="Last Modified" value="lastUpdated"/>
              </Workbook.Sheet>
            </Workbook>
          </div>
      </div>;
    } else {
      productsRender = <div></div>;
    }
    return (
      productsRender
    );
  }
}

const mapStateToProps = (state) => {
  return {
      products: state.products,
      loading: state.loading,
      error: state.error
  }
}
export default connect(mapStateToProps)(AllProducts);