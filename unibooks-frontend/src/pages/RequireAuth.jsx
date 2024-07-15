import { useLocation, Navigate, Outlet } from "react-router-dom";
import useAuth from "../hooks/useAuth";

const RequireAuth =()=>{
    const {auth} = useAuth();
    const location = useLocation();

    return (
        auth?.user 
        ? <Outlet/>   //return any child components of require auth
        : <Navigate to = "/login" state={{from: location}} replace/>
    );
}

export default RequireAuth