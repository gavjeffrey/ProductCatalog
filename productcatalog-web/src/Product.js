import React, { Component } from 'react';
import { connect } from 'react-redux';

class Product extends Component {
    HandleDelete = (e) => {
        e.preventDefault();
        const id = this.props.product.id;
        fetch('http://localhost:30649/api/product/' + id, {
        method: 'DELETE',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        }
        })
        .then((result) => {
            this.props.dispatch({ type: 'DELETE_PRODUCT', id: this.props.product.id });
        },
        (error) => {
          this.setState({
            error
          });
        })        
    }
  
    render() {
        return (
        <div className="product">
            <div className="header_image clearfix">
                <img src={"data:image/png;base64," + this.props.product.photo} />&nbsp;
                <h2 className="product_title">{this.props.product.name}</h2>
            </div>
            <p className="product_price">Price: {this.props.product.price}</p>
            <div className="control-buttons">
                <button className="edit"
                onClick={() => this.props.dispatch({ type: 'EDIT_PRODUCT', id: this.props.product.id })
                }
                >Edit</button>
                <button className="delete"
                onClick={this.HandleDelete}
                >Delete</button>
            </div>
        </div>
        );
    }
}
export default connect()(Product);