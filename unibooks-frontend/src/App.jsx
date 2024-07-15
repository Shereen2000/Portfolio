import { Route, Routes } from 'react-router-dom'
import './App.css'
import AdvertList from './components/AdvertList'
import Header from './components/Header'
import Layout from './pages/layout'
import LoginPage from './pages/LoginPage'
import BookmarksPage from './pages/BookmarksPage'
import MyAdvertsPage from './pages/MyAdvertsPage'
import RegisterPage from './pages/RegisterPage'
import HomePage from './pages/HomePage'
import RequireAuth from './pages/RequireAuth';


function App() {

  return (
    <Routes>
        <Route path = '/' element = {<Layout/>}>
              <Route path = "login" element={<LoginPage/>}></Route>
              <Route path = "bookmarks" element = {<BookmarksPage/>}></Route>
              <Route path = "register" element = {<RegisterPage/>}></Route>
              <Route path = "myadverts" element = {<MyAdvertsPage/>}></Route>
              <Route path = "/" element = {<HomePage/>}></Route>

              <Route element ={<RequireAuth/>}>

              </Route>

        </Route>
    </Routes>
   
  )
}

export default App
