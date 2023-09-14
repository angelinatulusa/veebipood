import logo from './logo.svg';
import './App.css';

function App() {
  const [tooted, setTooted] = useState([]);

  useEffect(() => {
    fetch("https://localhost:7168/tooted")
      .then(res => res.json())
      .then(json => setTooted(json));
  }, []);

  function kustuta(index) {
    fetch("https://localhost:7168/kustuta/" + index)
      .then(res => res.json())
      .then(json => setTooted(json));
  }

  return (
    <div className="App">
      {tooted.map((toode, index) => 
        <div>
          <div>{toode.id}</div>
          <div>{toode.name}</div>
          <div>{toode.price.toFixed(2)}</div>
          <button onClick={() => kustuta(index)}>x</button>
        </div>)}
    </div>
  );
}

export default App;
