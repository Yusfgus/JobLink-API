# 1️⃣ Authentication

## Register

> Register new job seeker

```json
POST /api/v1/auth/register/job-seeker

{
    "email": "string",
    "password": "string",
    "firstName": "string",
    "lastName": "string",
    "gender": "string",
}
```

**Response**
```json
{
    "AccessToken": "string",
    "RefreshToken": "string"
}
```

> Register new company

```json
POST /api/v1/auth/register/company

{
    "email": "string",
    "password": "string",
    "name": "string",
    "industry": "string",
}
```

**Response**
```json
{
    "AccessToken": "string",
    "RefreshToken": "string"
}
```

---

## LogIn

> Login job seeker

```json
POST /api/v1/auth/login

{
    "email": "string",
    "password": "string",
}
```

**Response**
```json
{
    "AccessToken": "string",
    "RefreshToken": "string"
}
```
---

# 2️⃣ Job Seeker

## General Info

> Get general information.

```
GET /api/v1/job-seekers/me/profile
```

**Response**

```json
{
    "firstName": "string",
    "middleName": "string",
    "lastName": "string",
    "email": "string",
    "mobileNumber": "string",
    "birthdate": "string",
    "gender": "string",
    "nationality": "string",
    "maritalStatus": "string",
    "militaryStatus": "string",
    "country": "string",
    "city": "string",
    "area": "string",
}
```

> Update general information.

```json
PUT /api/v1/job-seekers/me/profile

{
    "firstName": "string",
    "middleName": "string",
    "lastName": "string",
    "email": "string",
    "mobileNumber": "string",
    "birthdate": "string",
    "gender": "string",
    "nationality": "string",
    "maritalStatus": "string",
    "militaryStatus": "string",
    "country": "string",
    "city": "string",
    "area": "string",
}
```

---

## Skills

> Add a skill.

```json
POST /api/v1/job-seekers/me/skills

{
    "skillId": "string",
    "level": "string",
}
```

> Get all skills.

```
GET /api/v1/job-seekers/me/skills
```

**Response**

```json
[
    {
        "id": "string",
        "name": "string",
        "level": "string",
    }
]
```

> Get a skill by id.

```
GET /api/v1/job-seekers/me/skills/{id}
```

**Response**

```json
{
    "id": "string",
    "name": "string",
    "level": "string",
}
```

> Update a skill by id.

```json
PUT /api/v1/job-seekers/me/skills/{id}

{
    "name": "string",
    "level": "string",
}
```

> Delete a skill by id.

```
DELETE /api/v1/job-seekers/me/skills/{id}
```

---

## Education

> Add an education.

```json
POST /api/v1/job-seekers/me/educations

{
    "degree": "string",
    "institution": "string",
    "fieldOfStudy": "string",
    "grade": "string",
    "country": "string",
    "startYear": "string",
    "endYear": "string",
}
```

> Get all educations.

```
GET /api/v1/job-seekers/me/educations
```

**Response**

```json
[
    {
        "id": "string",
        "degree": "string",
        "institution": "string",
        "country": "string",
        "startYear": "string",
        "endYear": "string",
    }
]
```

> Get an education details by id.

```
GET /api/v1/job-seekers/me/educations/{id}
```

**Response**

```json
{
    "id": "string",
    "degree": "string",
    "institution": "string",
    "fieldOfStudy": "string",
    "grade": "string",
    "country": "string",
    "startYear": "string",
    "endYear": "string",
}
```

> Update an education by id.

```json
PUT /api/v1/job-seekers/me/educations/{id}

{
    "degree": "string",
    "institution": "string",
    "fieldOfStudy": "string",
    "grade": "string",
    "country": "string",
    "startYear": "string",
    "endYear": "string",
}
```

> Delete an education by id.

```
DELETE /api/v1/job-seekers/me/educations/{id}
```

---

## Experience

> Add an experience.

```json
POST /api/v1/job-seekers/me/experiences

{
    "company": "string",
    "position": "string",
    "description": "string",
    "country": "string",
    "salary": "string",
    "startDate": "string",
    "endDate": "string",
}
```

> Get all experiences.

```
GET /api/v1/job-seekers/me/experiences
```

**Response**

```json
[
    {
        "id": "string",
        "company": "string",
        "position": "string",
        "country": "string",
        "startDate": "string",
        "endDate": "string",
    }
]
```

> Get an experience details by id.

```
GET /api/v1/job-seekers/me/experiences/{id}
```

**Response**

```json
{
    "id": "string",
    "company": "string",
    "position": "string",
    "description": "string",
    "country": "string",
    "salary": "string",
    "startDate": "string",
    "endDate": "string",
}
```

> Update an experience.

```json
PUT /api/v1/job-seekers/me/experiences/{id}

{
    "company": "string",
    "position": "string",
    "description": "string",
    "country": "string",
    "salary": "string",
    "startDate": "string",
    "endDate": "string",
}
```

> Delete an experience.

```
DELETE /api/v1/job-seekers/me/experiences/{id}
```

---

## Resume

> Add resume.

```json
POST /api/v1/job-seekers/me/resume

{
    "fileUrl": "string",
}
```

> Get resume.

```
GET /api/v1/job-seekers/me/resume
```

**Response**

```json
{
    "fileUrl": "string",
}
```

> Update resume.

```
PUT /api/v1/job-seekers/me/resume
```

**Body**

```json
{
    "fileUrl": "string",
}
```

> Delete resume.

```
DELETE /api/v1/job-seekers/me/resume
```

---

## Applications

> Get all applications.

```
GET /api/v1/job-seekers/me/applications
```

**Response**

```json
[
    {
        "id": "string",
        "jobTitle": "string",
        "companyName": "string",
        "companyProfilePictureUrl": "string",
        "location": "string",
        "appliedAt": "string",
        "status": "string",
    }
]
```

> Get an application details by id.

```
GET /api/v1/job-seekers/me/applications/{id}
```

**Response**

```json
{
    "id": "string",
    "jobTitle": "string",
    "jobType": "string",
    "experienceLevel": "string",
    "salaryRange": "string",
    "companyName": "string",
    "companyProfilePictureUrl": "string",
    "location": "string",
    "locationType": "string",
    "postedAt": "string",
    "appliedAt": "string",
    "status": "string",
}
```

> Withdraw an application details by id.

```
DELETE /api/v1/job-seekers/me/applications/{id}
```


---

## Saved Jobs

> Get all saved jobs.

```
GET /api/v1/job-seekers/me/saved-jobs
```

**Response**

```json
[
    {
        "id": "string",
        "jobTitle": "string",
        "jobType": "string",
        "jobType": "string",
        "companyName": "string",
        "companyProfilePictureUrl": "string",
        "location": "string",
        "appliedAt": "string",
        "status": "string",
    }
]
```


> Unsave saved job by id.

```
DELETE /api/v1/job-seekers/me/saved-jobs/{id}
```

---
