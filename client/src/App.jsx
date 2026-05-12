import { useEffect, useState } from "react";
import { getBoard } from "./api";

function App() {
  const [board, setBoard] = useState(null);

  useEffect(() => {
    getBoard(1).then(setBoard).catch(console.error);
  }, []);

  return (
    <div>
      <h1>Board Test</h1>
      <pre>{JSON.stringify(board, null, 2)}</pre>
    </div>
  );
}

export default App;