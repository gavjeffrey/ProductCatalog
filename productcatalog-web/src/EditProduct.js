import React, { Component } from 'react';
import { connect } from 'react-redux';

class EditProduct extends Component {
handleEdit = (e) => {
  e.preventDefault();
debugger
  const id = this.props.product.id;
  const newName = this.getName.value;
  const newPrice =  this.getPrice.value;
  let photo =  this.state.image.replace(/^data:image\/[a-z]+;base64,/, "");

  let jsonData = {Id: id, name: newName, price: newPrice, photo: photo };

  fetch('http://localhost:30649/api/product', {
    method: 'POST',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(jsonData)
  })
  .then(
    (result) => {
      const data = {
        id,
        newName,
        newPrice,
        photo,
        editing: false
      };
      this.props.dispatch({ type: 'UPDATE', id: id, data: data });
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
        <div key={this.props.product.id} className="product">
            <form className="form" onSubmit={this.handleEdit} encType="multipart/form-data" method="POST">
                <input required type="text" ref={(input)=>this.getName = input} defaultValue={this.props.product.name}
                placeholder="Enter Product Name"/>
                <br /><br />
                <input required type="number" min="0.01" step="0.01" ref={(input)=>this.getPrice = input} defaultValue={this.props.product.price}
                placeholder="Enter Product Price" />
                <br /><br />
                <input required type="file" ref={(input)=>this.getFile = input} onChange={this.onImageChange.bind(this)}
                    placeholder="Change Product Image" />
                <br /><br />
                <button>Update</button>
            </form>
        </div>
    );
}
}
export default connect()(EditProduct);