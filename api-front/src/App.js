import logo from './logo.svg';
import './App.css';
import { useState } from 'react';

function App() {
  const [products, setProducts] = useState([]);
  const [isUsd, setUsd] = useState(false);

  useEffect(() => {
    fetch("https://localhost:7168/api/Products")
      .then(res => res.json())
      .then(json => setProducts(json));
  }, []);

  function kustuta(index) {
    fetch("https://localhost:7168/api/Products/kustuta/" + index)
      .then(res => res.json())
      .then(json => setProducts(json));
  }

  return (
    <div className="App">
      {products.map((toode, index) => 
        <div>
          <div>{toode.id}</div>
          <div>{toode.name}</div>
          <div>{toode.price.toFixed(2)}</div>
          <button onClick={() => kustuta(index)}>kustuta</button>
        </div>)}
    </div>
  );
}

export default App;
