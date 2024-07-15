import { createContext, useState } from "react";
import Login from "../pages/LoginPage";

const AuthContext = createContext({});

export const AuthProvider = ({children}) => {
    const [auth, setAuth] = useState({});
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    const login = () => {
        setIsLoggedIn(true);
    };

    const Logout = () => {
        setIsLoggedIn(false);
    }

    return (
        <AuthContext.Provider value = {{auth, setAuth, isLoggedIn, Logout, Login}}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthContext;