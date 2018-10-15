import {
  FETCH_PRODUCTS_BEGIN,
  FETCH_PRODUCTS_SUCCESS,
  FETCH_PRODUCTS_FAILURE
} from '../ProductActions';

const initialState = {
  products: [],
  loading: false,
  error: null
};

const productReducer = (state = initialState, action) => {
    switch(action.type) {
      case FETCH_PRODUCTS_BEGIN:
      return {
        ...state,
        loading: true,
        error: null
      };

    case FETCH_PRODUCTS_SUCCESS:
      return {
        ...state,
        loading: false,
        products: action.payload
      };

    case FETCH_PRODUCTS_FAILURE:
      return {
        ...state,
        loading: false,
        error: action.payload,
        products: []
      };

      case 'ADD_PRODUCT':
      debugger
        return {
          ...state,
          loading: false,
          error: null,
          products: state.products.concat([action.data])
        };
      case 'DELETE_PRODUCT':
        return {
          ...state,
          loading: false,
          error: null,
          products: state.products.filter((product)=>product.id !== action.id)
        };
      case 'EDIT_PRODUCT':
        return {
          ...state,
          loading: false,
          error: null,
          products: state.products.map((product)=>product.id === action.id ? {...product,editing:!product.editing}:product)
        };
      case 'UPDATE':
        return {
          ...state,
          loading: false,
          error: null,
          products: state.products.map((product)=>{
            if(product.id === action.id) {
              return {
                 ...product,
                 name:action.data.newName,
                 price:action.data.newPrice,
                 photo:action.data.newImage,
                 editing: !product.editing
              }
            } else return product;
          })
        };        
      default:
        return state;
    }
  }
  export default productReducer;