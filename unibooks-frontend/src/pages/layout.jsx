import { Outlet } from "react-router-dom"
import Header from "../components/Header"
import Footer from "../components/Footer"


const Layout = () => {



  return (
    <main className="w-full">
        <Header/>
        <Outlet/>
    </main>)
}

export default Layout
