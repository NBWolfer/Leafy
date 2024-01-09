import Auth from "../Context/AuthContext";''
import { useLocation, Navigate } from "react-router-dom";



const RequireAuth = () => {
    const { status, name, info} = Auth.AuthContext();
    const location = useLocation();
    
        return(
            status? 
            <Navigate to="/login" />  /> 
            : <Navigate to={location.pathname}
        )


}






