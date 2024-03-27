import "./styles.css";
import { useState } from "react";

/*
  INSTRUCTIONS:
  Create a "todo"(add cities) app with the following criteria.
    1. The user can add new cities
    2. The user can remove existing cities items
*/

function TodoItem ({todo, onDelete}) {
  return (
    <li>
      <span>{todo.text}</span>
      <button onClick={() => onDelete(todo.id)}>Delete</button>
    </li>
  );
}

function TodoList ({todos, onDelete}){
  return (
    <ul>
      {todos.map((todo) => 
        <TodoItem key={todo.id} todo={todo} onDelete={onDelete}></TodoItem>
      )}
      
    </ul>
  )
}

export default function App() {

  const [cities, setCities] = useState([]);
  const [inputValue, setInputValue] = useState("");

  const addCities = () => {
    //Complete function
    if (inputValue && inputValue.trim()) {
      setCities([
        ...cities,
        { id: Math.random().toString(36).substring(2, 15), text: inputValue },
      ]);
      setInputValue("");
    }
  };

  const handleInputChange = (event) => {
    if(event.key === 'Enter'){
      setInputValue(event.target.value);
      addCities();  
    }
    setInputValue(event.target.value);
  }

  return (
    <div className="App">
      <h1>Todo List</h1>
      <input type="text" value={inputValue} onChange={handleInputChange} onKeyDown={handleInputChange} />
      <button onClick={addCities}>Add City</button>

      <ul>
        {cities.map((city) => (
          <li key={city.id}>
            {city.text}
            <button
              onClick={() => setCities(cities.filter((c) => c.id !== city.id))}
            >            
              Remove City
            </button>
          </li>
        ))}
      </ul>
    </div>
  );
}
