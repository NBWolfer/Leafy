import { useEffect, useState } from 'react';
import axios from 'axios';

interface User {
    id: number,
    name: string,
    email: string,
    password: string,
    role: string,
    registeredDate: string,
}

function User() {
    const [users, setUsers] = useState<User[]>([]);

    

    useEffect(() => {
        const fetchUsers = async () => {
            await axios.get(`api/Users`).then(response => {
                console.log(response);
                const users = response.data.data.json();
                setUsers(users);
            }).catch(error => {
                console.log(error);
            });
        }
        fetchUsers();
    }, []);

    const content = users === undefined ?
        <p>Loading...</p> :
        <ul>
            {users.map(user =>
                <li key={user.id}>
                    <h3>{user.name}</h3>
                    <p>{user.email}</p>
                    <p>{user.password}</p>
                    <p>{user.role}</p>
                    <p>{user.registeredDate}</p>
                </li>
            )}
        </ul>;




    return (
        <div>
            <h1>Users</h1>
            {content}

        </div>
    )
}

export default User;  