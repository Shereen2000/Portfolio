import { useRef, useState, useEffect,} from 'react';
import useAuth from '../hooks/useAuth'
import {Link, useNavigate, useLocation } from "react-router-dom";
import axios from '../api/axios';

const LOGIN_URL = 'http://localhost:5222/api/account/login';

const Login = () => {
    
const {auth,setAuth} = useAuth();
const navigate = useNavigate();
const location= useLocation();
const from = location.state?.from?.pathname || "/";

const userRef = useRef();
const errRef = useRef();

const [user, setUser] = useState('');
const [pwd, setPwd] = useState('');
const [errMsg, setErrMsg] = useState('');
const [success, setSuccess] = useState(false);

useEffect(() => {
    userRef.current.focus();
}, [])

useEffect(() => {
  setErrMsg('');
}, [user, pwd])

const handleSubmit = async (e) => {
    e.preventDefault();
  try {
      const response = await axios.post(LOGIN_URL,
          JSON.stringify({ username:user, password:pwd }),
          {
              headers: { 'Accept': '*/*','Content-Type': 'application/json' },
          }
      );

      const accessToken = response?.data?.token;
      const roles = response?.data?.role;

      setAuth({ user, pwd, roles, accessToken });
      setUser('');
      setPwd('');
      setSuccess(true);
    

  } catch (err) {
      console.log(err)
      if (!err?.response) {
        setErrMsg('No Server Response');
    } else if (err.response?.status === 400) {
        setErrMsg('Missing Username or Password');
    } else if (err.response?.status === 401) {
        setErrMsg('Unauthorized');
    } else {
        setErrMsg('Login Failed');
    }
    errRef.current.focus();
}

}

  return (
    <div className='flex justify-center h-screen bg-gray-100 pt-6'>
        <div className='max-w-md w-full p-6 bg-white rounded-lg shadow-md h-fit'> 
            {success? (
                         navigate(from, {replace:true})
            ) : (
                <section>
                    <p ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</p>
              
                    <form onSubmit={handleSubmit}>
                      <div className='mb-4'>
                            <label htmlFor="username" className='block text-sm font-medium text-gray-700'>Username:</label>
                            <input
                                type="text"
                                id="username"
                                ref={userRef}
                                autoComplete="off"
                                onChange={(e) => setUser(e.target.value)}
                                value={user}
                                required
                                className='mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm'
                            />
                      </div>
                      <div className='mb-4'>
                                <label htmlFor="password" className='block text-sm font-medium text-gray-700'>Password:</label>
                                <input
                                    type="password"
                                    id="password"
                                    onChange={(e) => setPwd(e.target.value)}
                                    value={pwd}
                                    required
                                     className='mt-1 block w-full px-3 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm'
                                />
                       </div >

                        <div className='mb-6'>
                                <button
                                    type='submit'
                                    className='w-full bg-blue-500 text-white py-2 px-4 rounded-md hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50'
                                >Sign In</button>
                        </div>   
                    </form>
                        <div className='text-center'>
                              <p>
                                  Need an Account? :
                                  <span className="line">
                                      <Link to = "/register" className='text-blue-500'> Register</Link>
                                  </span>
                              </p>
                      </div>
                </section>
            )}
            </div>
        </div>
  )
}

export default Login
