import React from 'react'
import AdvertCard from './AdvertCard'
import '../components/styles.css'

const AdvertList = () => {

  const x  = [1,1,2,3,4,4,5,5,6,7,4,1,2,3,4,4,5,3,5,3,5,6,6,6,7,8,4,7,3,6,3,6,2,5,7,9,3]
  return (
    <div className='p-5 custom-height overflow-y-auto '>
    
          {x.map((item, index) => (
            <AdvertCard key={index} />
          ))}
    
    </div>
  )
}

export default AdvertList 
