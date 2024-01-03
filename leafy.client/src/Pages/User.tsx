import Cookies from 'js-cookie';
import { useEffect, useState } from 'react';
import axios from 'axios';
import { redirect } from 'react-router-dom';

interface User {
    id: number,
    name: string,
    email: string,
    password: string,
    role: string,
    registeredDate: string,
}

function User() {
    const token = Cookies.get('token');
    const [users, setUsers] = useState<User[]>([]);
    console.log(token);
    if (token === undefined) {
        redirect('/login');
    }

    useEffect(() => {
        const fetchUsers = async () => {
            await axios.get(`api/Users`, {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }).then(response => {
                console.log(response.headers.getAuthorization?.toString());
                const users = response.data;
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