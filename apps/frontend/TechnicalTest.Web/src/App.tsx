import { useEffect, useState } from 'react';
import type {UserUI} from './types/user';
import { getUsers } from './services/userService';
import { UserTable } from './components/UserTable';
import './App.css';

export function App() {
  const [users, setUsers] = useState<UserUI>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  
  useEffect(() => {
    getUsers()
        .then((data) => {
          setUsers(data);
          setLoading(false);
        })
        .catch((err) => {
          setError(err.message);
          setLoading(false);
        });
  }, []);

  const handleDelete = (id: string) => {
    setUsers((prevUsers) => prevUsers.filter((u) => u.id !== id));
  };
  
  return (
      <div className={"container"}>
        <h1>Gestión de Usuarios</h1>

        {loading && <p> Cargando usuarios desde la API...</p>}

        {error && <p className="error"> Error: {error}</p>}

        {!loading && !error && (
            <UserTable users={users} onDelete={handleDelete} /> 
        )}
      </div>
  );
};

