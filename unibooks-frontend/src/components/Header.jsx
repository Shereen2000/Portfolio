import React, { useState } from 'react'
import {HiDotsVertical} from 'react-icons/hi'
import { FcAdvertising, FcBookmark } from "react-icons/fc";
import { FaBookmark } from "react-icons/fa";
import { FaToggleOn } from "react-icons/fa";
import { FaToggleOff } from "react-icons/fa";
import {
    HiMagnifyingGlass,
    HiHome
   } from 'react-icons/hi2';


const Header = () => {
    const [searchBy, setSearchBy] = useState('Title');
  

    const handleSelectChange = (e) => {
        setSearchBy(e.target.value); // Update state with selected option
      };

  return (
<div className='p-5'>
    <div className='flex justify-between items-center'>
        <div className='text-3xl font-serif'>
            UNIBOOKS
        </div>

        <div className='hidden gap-5 md:flex'>
            <div className='flex items-center gap-1'> <a href="">Home</a><HiHome/></div>
            <div className='flex items-center gap-1'> <a href="">Advertise</a><FcAdvertising/></div>
            <div className='flex items-center gap-1' > <a href="">BookMarks</a><FcBookmark/></div>
            <div className='flex items-center gap-1'> <a href="">Login</a><FaToggleOff/></div>
           
        </div>       
        <div className='md:hidden flex gap-4'>
            <div className='flex'> <a href=""><HiHome/></a></div>
            <div> <a href=""><FcAdvertising/></a></div>
            <div> <a href=""><FcBookmark/></a></div>
            <div> <a href=""><FaToggleOff/></a></div>
            
          
        </div>
    </div>

   <div className='flex p-5 items-center justify-center gap-4'> 
        <select value={searchBy} onChange={handleSelectChange}>
            <option value="Title" >Title</option>
            <option value="Author">Author</option>
            <option value="Isbn">Isbn</option>
        </select>
        <input type="text" className='border border-solid rounded-full w-full md:w-96 p-2' placeholder={`Search ${searchBy}`} />
               <HiMagnifyingGlass/>
   </div>
   
    
    
</div>
  )
}

export default Header
