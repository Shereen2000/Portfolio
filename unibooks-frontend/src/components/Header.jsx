import React from 'react';
import { HiDotsVertical } from 'react-icons/hi';
import { FcAdvertising, FcBookmark } from 'react-icons/fc';
import { FaBookmark } from 'react-icons/fa';
import { FaToggleOn } from 'react-icons/fa';
import { FaToggleOff } from 'react-icons/fa';
import { HiHome } from 'react-icons/hi2';
import { Link } from 'react-router-dom';
import useAuth from '../hooks/useAuth';

const Header = () => {
  const { auth } = useAuth();

  return (
    <div className='p-5 bg-blue-500 text-white'>
      <div className='container mx-auto flex justify-between items-center'>
        <div className='text-3xl font-serif'>UNIBOOKS</div>

        <div className='hidden gap-5 md:flex'>
          <div className='flex items-center gap-1'>
            <Link to='/'>
              <span className='hover:underline'>Home</span>
            </Link>
            <HiHome className='text-xl' />
          </div>
          <div className='flex items-center gap-1'>
            <Link to='/myadverts'>
              <span className='hover:underline'>My Adverts</span>
            </Link>
            <FcAdvertising className='text-xl' />
          </div>
          <div className='flex items-center gap-1'>
            <Link to='/bookmarks'>
              <span className='hover:underline'>Bookmarks</span>
            </Link>
            <FcBookmark className='text-xl' />
          </div>
          {auth?.user ? (
            <div className='flex items-center gap-1'>
              <Link to='/logout'>
                <span className='hover:underline'>Log Off</span>
              </Link>
              <FaToggleOn className='text-xl' />
            </div>
          ) : (
            <div className='flex items-center gap-1'>
              <Link to='/login'>
                <span className='hover:underline'>Login</span>
              </Link>
              <FaToggleOff className='text-xl' />
            </div>
          )}
        </div>
        <div className='md:hidden flex gap-4'>
          <div className='flex'>
            <a href=''>
              <HiHome className='text-xl' />
            </a>
          </div>
          <div>
            <a href=''>
              <FcAdvertising className='text-xl' />
            </a>
          </div>
          <div>
            <a href=''>
              <FcBookmark className='text-xl' />
            </a>
          </div>
          <div>
            <a href=''>
              <FaToggleOff className='text-xl' />
            </a>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Header;