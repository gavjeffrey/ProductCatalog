import React, { Component } from 'react';
import {connect} from 'react-redux';

class CreateProduct extends Component {
    
    handleSubmit = (e) => {
        e.preventDefault();
        
        let id=0;
        const name = this.getName.value;
        const price =  this.getPrice.value;
        let photo =  this.state.image.replace(/^data:image\/[a-z]+;base64,/, "");

        let jsonData = {name: name, price: price, photo: photo };
        
        fetch('http://localhost:30649/api/product', {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(jsonData)
          })
          .then(res => res.json())
          .then(
            (result) => {
              id = result.id;
              const data = {
                id,
                name,
                price,
                photo,
                editing:false
            }
            this.props.dispatch({
                type:'ADD_PRODUCT',
                data});

              this.getName.value = '';
              this.getPrice.value = '';
              this.getFile.value = '';
            },
            (error) => {
              this.setState({
                error
              });
            }
          );
    }

    onImageChange(event) {
        if (event.target.files && event.target.files[0]) {
            let reader = new FileReader();
            reader.onload = (e) => {
                this.setState({image: e.target.result});
            };
            reader.readAsDataURL(event.target.files[0]);
        }
    }

    render() {
        return (
            <div  className="product-container">
            <h1 className="product_heading">Add new product</h1>
            <form onSubmit={this.handleSubmit} encType="multipart/form-data" method="POST">
                <input required type="text" ref={(input)=>this.getName = input} 
                    placeholder="Enter Product Name"/>
                <br /><br />
                <input required type="number" min="0.01" step="0.01" ref={(input)=>this.getPrice = input} 
                    placeholder="Enter Product Price" />
                <br /><br />
                <input required type="file" ref={(input)=>this.getFile = input} onChange={this.onImageChange.bind(this)}
                    placeholder="Add Product Image" />
                <br /><br />
                <button>Add</button>
            </form>
            </div>
            );
        }
}
export default connect()(CreateProduct);