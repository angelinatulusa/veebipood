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

  useEffect(() => {
    fetch("https://localhost:7168/Products")
      .then(res => res.json())
      .then(json => setTooted(json));
  }, []);

  function kustuta(index) {
    if (index >= 0 && index < tooted.length) {
      const id = tooted[index].id;
      fetch(`https://localhost:7168/Products/kustuta/${id}`, { method: "DELETE" })
      .then(res => {
        if (res.status === 204) {
          console.log("Toode edukalt kustutatud");
          setTooted(prevTooted => prevTooted.filter(toode => toode.id !== id));
        } else if (res.status === 404) {
          console.error("Toodet ei leitud");
        } else {
          console.error("Toote kustutamisel ilmnes viga");
        }
      })
      .catch(error => {
        console.error("Viga päringu tegemisel:", error);
      });
  } else {
    console.error("Kehtetu indeks toote kustutamiseks.");
  }

}
function lisa() {
  const newProduct = {
    id: Number(idRef.current.value),
    name: nameRef.current.value,
    price: Number(priceRef.current.value),
    aktiivne: isActiveRef.current.checked,
    stock: Number(stockRef.current.value)
  };

  fetch("https://localhost:7168/Products/lisa", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(newProduct)
  })
    .then(res => res.json())
    .then(json => setTooted(json));
}

  function dollariteks() {
    const kurss = 1.1;
    setUsd(true);
    fetch("https://localhost:7168/Products/hind-dollaritesse/" + kurss, { method: "PATCH" })
      .then(res => res.json())
      .then(json => setTooted(json));
  }

  function eurodeks() {
    const kurss = 0.9091;
    setUsd(false);
    fetch("https://localhost:7168/Products/hind-eurosse/" + kurss, { method: "PATCH" })
      .then(res => res.json())
      .then(json => setTooted(json));
  }

  function maksa(sum) {
    fetch(`https://localhost:7168/Products/pay/${sum}`)
      .then(res => {
        if (!res.ok) {
          throw new Error('Network response was not ok');
        }
        return res.json();
      })
      .then(paymentLink => {
        window.location.href = paymentLink;
      })
      .catch(error => {
        console.error("Viga maksmisel:", error);
      });
  }

  return (
    <div className="App">
      <label>ID</label> <br />
      <input ref={idRef} type="number" /> <br />
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
