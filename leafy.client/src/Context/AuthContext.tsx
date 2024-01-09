import axios from "axios";
import React from "react";
import { useEffect, useState } from "react";


interface User {
    status: boolean,
    info: string,
    name: string,
}

function AuthContext() {
  const [status, setStatus] = useState<boolean>(false);
  const [name, setName] = useState<string>("");
  const [info, setInfo] = useState<string>("");

  useEffect (() => {
    const fetchData = async () => {
      await axios.post(`api/Auth/getStatus`).then(response => {
        console.log(response);
        setStatus(response.data.status);
        setName(response.data.name);
        setInfo(response.data.info);
      }).catch(() => {
        setStatus(false);
        setName("");
        setInfo("");
        alert("Can't Auth That users Logged in");
      });
      fetchData();


    };

  })

  return {status, name, info};



}


export default {AuthContext };
