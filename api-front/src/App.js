import { useEffect, useRef, useState } from 'react';
import './App.css';

function App() {
  const [products, setProducts] = useState([]);
  const idRef = useRef();
  const nameRef = useRef();
  const priceRef = useRef();
  const stockRef = useRef();
  const isActiveRef = useRef();
  const summString = null;
  const [isUsd, setUsd] = useState(false);

  useEffect(() => {
    fetch("https://localhost:7168/Products")
      .then(res => res.json())
      .then(json => setProducts(json));
  }, []);

  function kustuta(index) {
    fetch("https://localhost:7168/Products/kustuta/" + index)
    .then(res => res.json())
    .then(json => setProducts(json));
  }

  function lisa() {
    fetch(`https://localhost:7168/Products/lisa/${Number(idRef.current.value)}/${nameRef.current.value}/${(priceRef.current.value)}/${(isActiveRef.current.checked)}/${stockRef.current.value}`, {"method": "POST"})
      .then(res => res.json())
      .then(json => setProducts(json));
  }

  function dollariteks() {
    const kurss = 1.1;
    setUsd(true);
    fetch("https://localhost:7168/Products/hind-dollaritesse/" + kurss)
      .then(res => res.json())
      .then(json => setProducts(json));
  }

  function eurodeks() {
    const kurss = 0.9091;
    setUsd(false);
    fetch("https://localhost:7168/Products/hind-dollaritesse/" + kurss)
      .then(res => res.json())
      .then(json => setProducts(json));
  }

  return (
    <div className="App">
      <label>ID</label> <br />
      <input ref={idRef} type="number" /> <br />
      <label>Name</label> <br />
      <input ref={nameRef} type="text" /> <br />
      <label>Price</label> <br />
      <input ref={priceRef} type="number" /> <br />
      <label>Stock</label> <br />
      <input ref={stockRef} type="number" /> <br />
      <label>Active</label> <br />
      <input ref={isActiveRef} type="checkbox" /> <br />
      <button onClick={() => lisa()}>Lisa</button>
      <br/>
      {isUsd === false && <button onClick={() => dollariteks()}>Muuda dollariteks</button>}
      {isUsd === true && <button onClick={() => eurodeks()}>Muuda eurodeks</button>}
      {products.map((product, index) => 
        <table>
          <tr>
            <th>Id</th>
            <th>Name</th>
            <th>Price</th>
            <th>Stock</th>
            <th>Delete</th> 
            <th>Buy</th>
          </tr>
          
          <td>{product.id}</td>
          <td>{product.name}</td>
          <td>{product.price.toFixed(2)}</td>
          <td>{product.stock}</td>
          <td><button onClick={() => kustuta(index)}>Kustuta</button></td>
          {<td><button>Tellida</button></td> }
        </table>)}
    </div>
  );
}

export default App;
