const API_URL = import.meta.env.VITE_API_URL;

export async function getBoard(id) {
  const res = await fetch(`${API_URL}/api/boards/${id}`);
  if (!res.ok) throw new Error("Failed to fetch board");
  return res.json();
}

export async function getBoards() {
  const res = await fetch(`${API_URL}/api/boards`);
  if (!res.ok) throw new Error("Failed to fetch boards");
  return res.json();
}