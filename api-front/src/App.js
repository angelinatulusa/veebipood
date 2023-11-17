import React, { useEffect, useRef, useState } from 'react';
import './App.css';

function App() {
  const [tooted, setTooted] = useState([]);
  const [isUsd, setUsd] = useState(false);
  const idRef = useRef();
  const nameRef = useRef();
  const priceRef = useRef();
  const isActiveRef = useRef();
  const stockRef = useRef();

  const fetchProducts = async () => {
    try {
      const response = await fetch('https://localhost:7168/Products');
      const data = await response.json();
      const activeProducts = data.filter((toode) => toode.active);
      setTooted(data);
    } catch (error) {
      console.error('Error fetching products:', error);
    }
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  function kustuta(index) {
    if (index >= 0 && index < tooted.length) {
      const id = tooted[index].id;
      fetch(`https://localhost:7168/Products/kustuta/${id}`, { method: "DELETE" })
        .then(res => {
          if (res.status === 204) {
            console.log("Toode edukalt kustutatud");
            setTooted(prevTooted => prevTooted.filter(toode => toode.id !== id));
          }
        })
        .catch(error => {
          console.error("Viga päringu tegemisel:", error);
        });
        fetchProducts();
    } else {
      console.error("Kehtetu indeks toote kustutamiseks.");
    }
  }

  const lisa = async () => {
    const newProduct = {
      name: nameRef.current.value,
      price: Number(priceRef.current.value),
      isActive: isActiveRef.current.checked,
      stock: Number(stockRef.current.value)
    };

    try {
      await fetch('https://localhost:7168/Products/lisa', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(newProduct),
      });
      fetchProducts();
    } catch (error) {
      console.error('Error adding product:', error);
    }
  };

  function dollariteks() {
    const kurss = 1.1;
    setUsd(true);
    fetch("https://localhost:7168/Products/hind-dollaritesse/" + kurss, { method: "PATCH" })
      .then(res => res.json())
      .then(json => setTooted(json));
  }

  function eurodeks() {
    const kurss = 1.1;
    setUsd(false);
    fetch("https://localhost:7168/Products/hind-eurosse/" + kurss, { method: "PATCH" })
      .then(res => res.json())
      .then(json => setTooted(json));
  }

  async function maksa(summ) {
    try {
      const response = await fetch(`https://localhost:7168/Products/pay/${summ}`);
      if (response.ok) {
        let payLink = await response.text();
        payLink = payLink.replace(/^"|"$/g, '');
        window.open(payLink, '_blank');
      } else {
        console.error('Payment failed.');
      }
    } catch (error) {
      console.error('Error making payment:', error);
    }
  }

  return (
    <div className="App">
      <label>Nimi</label> <br />
      <input ref={nameRef} type="text" /> <br />
      <label>Hind</label> <br />
      <input ref={priceRef} type="number" /> <br />
      <label>Stock</label> <br />
      <input ref={stockRef} type="number" /> <br />
      <label>Aktiivne</label> <br />
      <input ref={isActiveRef} type="checkbox" /> <br />
      <button onClick={() => lisa()}>Lisa</button>
      {isUsd === false && <button onClick={() => dollariteks()} id='Dollar'>Muuda dollariteks</button>}
      {isUsd === true && <button onClick={() => eurodeks()} id='Dollar'>Muuda eurodeks</button>}
      {tooted.map((toode, index) => (
        <div key={index}>
          <table id="customers">
            <td>{toode.id}</td>
            <td>{toode.name}</td>
            <td>{toode.price}</td>
            <button onClick={() => kustuta(index)}>x</button>
            <button onClick={() => maksa(toode.price)}>maksa</button>
          </table>
        </div>
      ))}
    </div>
  );
}

export default App;
