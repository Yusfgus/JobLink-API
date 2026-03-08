# 1️⃣ Guest User Stories

### Register account

**User Story**

> As a guest, I want to create an account so that I can apply for jobs.

**Endpoints**

```
POST /api/v1/auth/register
POST /api/v1/auth/verify-email
POST /api/v1/auth/resend-verification
```

---

### Login

**User Story**

> As a guest, I want to login so that I can access my account.

**Endpoints**

```
POST /api/v1/auth/login
POST /api/v1/auth/refresh-token
POST /api/v1/auth/logout
GET  /api/v1/auth/me
```

---

### Reset password

**User Story**

> As a guest, I want to recover my password if I forget it.

**Endpoints**

```
POST /api/v1/auth/forgot-password
POST /api/v1/auth/reset-password
```

---

### Browse jobs

**User Story**

> As a guest, I want to browse jobs so that I can explore opportunities.

**Endpoints**

```
GET /api/v1/jobs
GET /api/v1/jobs/latest
GET /api/v1/jobs/trending
GET /api/v1/jobs/{jobId}
```

---

### Search jobs

**User Story**

> As a guest, I want to search jobs by keyword and filters.

**Endpoints**

```
GET /api/v1/jobs/search
```

Query examples:

```
?keyword=backend
&location=cairo
&salaryMin=10000
&experience=junior
```

---

### View companies

**User Story**

> As a guest, I want to view company information.

**Endpoints**

```
GET /api/v1/companies
GET /api/v1/companies/{companyId}
GET /api/v1/companies/{companyId}/jobs
```

---

# 2️⃣ Job Seeker User Stories

### Manage profile

**User Story**

> As a job seeker, I want to manage my profile so employers can see my information.

**Endpoints**

```
GET /api/v1/users/me
PUT /api/v1/users/me
GET /api/v1/jobseeker/profile
PUT /api/v1/jobseeker/profile
```

---

### Manage skills

**User Story**

> As a job seeker, I want to add my skills.

**Endpoints**

```
GET    /api/v1/skills
POST   /api/v1/jobseeker/skills
DELETE /api/v1/jobseeker/skills/{skillId}
```

---

### Manage education

**User Story**

> As a job seeker, I want to add my education history.

**Endpoints**

```
POST   /api/v1/jobseeker/education
PUT    /api/v1/jobseeker/education/{educationId}
DELETE /api/v1/jobseeker/education/{educationId}
```

---

### Manage experience

**User Story**

> As a job seeker, I want to add work experience.

**Endpoints**

```
POST   /api/v1/jobseeker/experience
PUT    /api/v1/jobseeker/experience/{experienceId}
DELETE /api/v1/jobseeker/experience/{experienceId}
```

---

### Upload resume

**User Story**

> As a job seeker, I want to upload my CV.

**Endpoints**

```
GET    /api/v1/resumes
POST   /api/v1/resumes
POST   /api/v1/resumes/{resumeId}/upload
PUT    /api/v1/resumes/{resumeId}
DELETE /api/v1/resumes/{resumeId}
```

---

### Apply for a job

**User Story**

> As a job seeker, I want to apply for jobs.

**Endpoints**

```
POST /api/v1/jobs/{jobId}/apply
GET  /api/v1/applications/me
GET  /api/v1/applications/{applicationId}
PATCH /api/v1/applications/{applicationId}/withdraw
```

---

### Save jobs

**User Story**

> As a job seeker, I want to save jobs to apply later.

**Endpoints**

```
GET    /api/v1/saved-jobs
POST   /api/v1/saved-jobs/{jobId}
DELETE /api/v1/saved-jobs/{jobId}
GET    /api/v1/saved-jobs/count
```

---

### Follow companies

**User Story**

> As a job seeker, I want to follow companies to track their jobs.

**Endpoints**

```
POST   /api/v1/companies/{companyId}/follow
DELETE /api/v1/companies/{companyId}/follow
```

---

### Get recommended jobs

**User Story**

> As a job seeker, I want personalized job recommendations.

**Endpoints**

```
GET /api/v1/jobs/recommended
```

---

### Notifications

**User Story**

> As a job seeker, I want notifications about applications and new jobs.

**Endpoints**

```
GET   /api/v1/notifications
PATCH /api/v1/notifications/{notificationId}/read
PATCH /api/v1/notifications/read-all
GET   /api/v1/notifications/unread-count
```

---

# 3️⃣ Employer User Stories

### Create company

**User Story**

> As an employer, I want to create a company profile.

**Endpoints**

```
POST /api/v1/companies
PUT  /api/v1/companies/{companyId}
```

---

### Post jobs

**User Story**

> As an employer, I want to create job posts.

**Endpoints**

```
POST   /api/v1/jobs
PUT    /api/v1/jobs/{jobId}
DELETE /api/v1/jobs/{jobId}
PATCH  /api/v1/jobs/{jobId}/close
PATCH  /api/v1/jobs/{jobId}/reopen
```

---

### Add job skills

**User Story**

> As an employer, I want to add skills required for a job.

**Endpoints**

```
POST   /api/v1/jobs/{jobId}/skills
DELETE /api/v1/jobs/{jobId}/skills/{skillId}
```

---

### View applicants

**User Story**

> As an employer, I want to view candidates who applied.

**Endpoints**

```
GET /api/v1/jobs/{jobId}/applications
GET /api/v1/applications/{applicationId}
GET /api/v1/resumes/{resumeId}/download
```

---

### Manage applications

**User Story**

> As an employer, I want to manage candidates.

**Endpoints**

```
PATCH /api/v1/applications/{applicationId}/accept
PATCH /api/v1/applications/{applicationId}/reject
PATCH /api/v1/applications/{applicationId}/shortlist
PATCH /api/v1/applications/{applicationId}/interview
```

---

### Job analytics

**User Story**

> As an employer, I want to see job statistics.

**Endpoints**

```
GET /api/v1/jobs/{jobId}/statistics
```

---

### Messaging candidates

**User Story**

> As an employer, I want to communicate with applicants.

**Endpoints**

```
POST /api/v1/messages/conversations
POST /api/v1/messages
GET  /api/v1/messages/{conversationId}
```

---

# 4️⃣ Admin User Stories

### Manage users

**User Story**

> As an admin, I want to manage platform users.

**Endpoints**

```
GET   /api/v1/admin/users
GET   /api/v1/users/{userId}
PATCH /api/v1/users/{userId}/ban
PATCH /api/v1/users/{userId}/unban
PATCH /api/v1/users/{userId}/activate
PATCH /api/v1/users/{userId}/deactivate
```

---

### Moderate jobs

**User Story**

> As an admin, I want to remove inappropriate jobs.

**Endpoints**

```
GET    /api/v1/admin/jobs
DELETE /api/v1/admin/jobs/{jobId}
```

---

### Manage companies

**User Story**

> As an admin, I want to approve companies.

**Endpoints**

```
GET   /api/v1/admin/companies
PATCH /api/v1/admin/companies/{companyId}/approve
PATCH /api/v1/admin/companies/{companyId}/reject
```

---

### Platform statistics

**User Story**

> As an admin, I want platform statistics.

**Endpoints**

```
GET /api/v1/admin/statistics
```

---

# 📊 Result

| Actor      | User Stories | Endpoints |
| ---------- | ------------ | --------- |
| Guest      | 6            | ~15       |
| Job Seeker | 10           | ~40       |
| Employer   | 6            | ~25       |
| Admin      | 4            | ~10       |

**Total endpoints ≈ 90–110**

---
