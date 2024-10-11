import React, { useState } from 'react';
import { useForm, SubmitHandler } from 'react-hook-form';

interface IFormInput {
  taskDescription: string;
  location: string;
  budget: number;
}

const PostTaskForm: React.FC = () => {
  const { register, handleSubmit, formState: { errors } } = useForm<IFormInput>();
  const [step, setStep] = useState(1);

  const onSubmit = async (data) => {  // //:SubmitHandler<IFormInput> = data => console.log(data);
    const response = await fetch('/api/post-task', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    });
  
    if (response.ok) {
      // Handle success
    } else {
      // Handle error
    }
  };

  const handleNext = () => setStep(step + 1);
  const handlePrevious = () => setStep(step - 1);

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      {step === 1 && (
        <div>
          <h2>Step 1: Task Details</h2>
          <input {...register("taskDescription", { required: true })} placeholder="What do you need done?" />
          {errors.taskDescription && <p>This field is required</p>}
        </div>
      )}

      {step === 2 && (
        <div>
          <h2>Step 2: Task Location</h2>
          <input {...register("location", { required: true })} placeholder="Enter location" />
          {errors.location && <p>This field is required</p>}
        </div>
      )}

      {step === 3 && (
        <div>
          <h2>Step 3: Payment & Review</h2>
          <input {...register("budget", { required: true, min: 1 })} type="number" placeholder="Enter your budget" />
          {errors.budget && <p>Please enter a valid budget</p>}
        </div>
      )}

      <div>
        {step > 1 && <button type="button" onClick={handlePrevious}>Back</button>}
        {step < 3 && <button type="button" onClick={handleNext}>Next</button>}
        {step === 3 && <button type="submit">Submit</button>}
      </div>
    </form>
  );

//   const Step1: React.FC = () => (
//     <div>
//       <h2>Step 1: Task Details</h2>
//       <input type="text" placeholder="What do you need done?" />
//     </div>
//   );
  
//   const Step2: React.FC = () => (
//     <div>
//       <h2>Step 2: Task Location</h2>
//       <input type="text" placeholder="Enter location" />
//     </div>
//   );
  
//   const Step3: React.FC = () => (
//     <div>
//       <h2>Step 3: Payment & Review</h2>
//       <input type="number" placeholder="Enter your budget" />
//     </div>
//   );
};

export default PostTaskForm;
