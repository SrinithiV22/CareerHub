--1
create database careerhub;
use careerhub;

--2,3
create table companies(
companyid int primary key,
companyname varchar(50),
location varchar(100));

create table jobs(
jobid int primary key,
companyid int,
foreign key(companyid) references companies(companyid),
jobtitle varchar(150),
jobdescription varchar(250),
joblocation varchar(50),
salary decimal(10,2),
jobtype varchar(50),
posteddate date);

create table applicants(
applicantid int primary key,
fname varchar(255),
lname varchar(255),
email varchar(255) unique,  
phone varchar(20),
resume varchar(255));

create table applications(
applicationid int primary key,
jobid int,
foreign key (jobid) references jobs(jobid),
applicantid int,
foreign key (applicantid) references applicants(applicantid),
applicationdate datetime,
coverletter varchar(255));

alter table jobs 
alter column posteddate datetime;

alter table applicants
add experience int;

insert into companies (companyid, companyname, location) values
(1, 'hexaware', 'chennai'),
(2, 'infosys', 'bangalore'),
(3, 'wipro', 'hyderabad'),
(4, 'hcl', 'noida'),
(5, 'zoho corporation', 'chennai'),
(6, 'tcs','kerala');

insert into jobs(jobid, companyid, jobtitle, jobdescription, joblocation, salary, jobtype, posteddate) values
(1, 1, 'qa engineer', 'responsible for software development and maintenance.', 'chennai', 835966.46, 'part-time', '2024-08-06 00:00:00'),
(2, 1, 'frontend developer', 'responsible for software development and maintenance.', 'chennai', 661453.74, 'full-time', '2024-05-04 00:00:00'),
(3, 1, 'full stack developer', 'responsible for software development and maintenance.', 'chennai', 677693.82, 'part-time', '2024-01-24 00:00:00'),
(4, 2, 'software developer', 'responsible for software development and maintenance.', 'bangalore', 520176.47, 'part-time', '2024-10-20 00:00:00'),
(5, 3, 'software developer', 'responsible for software development and maintenance.', 'hyderabad', 905086.57, 'full-time', '2024-04-14 00:00:00'),
(6, 3, 'frontend developer', 'responsible for software development and maintenance.', 'hyderabad', 844349.62, 'contract', '2024-07-08 00:00:00'),
(7, 4, 'full stack developer', 'responsible for software development and maintenance.', 'noida', 810887.29, 'full-time', '2024-03-17 00:00:00'),
(8, 5, 'network engineer', 'responsible for software development and maintenance.', 'chennai', 1115834.24, 'full-time', '2024-09-16 00:00:00'),
(9, 5, 'software developer', 'responsible for software development and maintenance.', 'chennai', 1158001.63, 'full-time', '2024-07-16 00:00:00'),
(10,6, 'software tester', 'responsible for software testing and reporting bugs.','kerala', 855000.45, 'full-time','2024-08-14 00:00:00');

insert into applicants (applicantid, fname, lname, email, phone, resume, experience) values
(1, 'ayush', 'sharma', 'ayush.sharma@gmail.com', '9876543210', 'resume1.pdf', 4),
(2, 'diya', 'patel', 'diya.patel@gmail.com', '7890654321', 'resume2.pdf', 2),
(3, 'rohit', 'verma', 'rohan.verma@gmail.com', '9012345678', 'resume3.pdf', 5),
(4, 'mathi', 'reddy', 'mathi.reddy@gmail.com', '7789012345', 'resume4.pdf', 3),
(5, 'karthik', 'iyer', 'karthik.iyer@gmail.com', '7654321098', 'resume5.pdf', 1);

insert into applications (applicationid, jobid, applicantid, applicationdate, coverletter) values
(1, 6, 1, '2024-05-01 00:00:00', 'dear hr, i am very interested in the role for job id 6.'),
(2, 5, 1, '2024-02-22 00:00:00', 'dear hr, i am very interested in the role for job id 5.'),
(3, 3, 1, '2024-01-03 00:00:00', 'dear hr, i am very interested in the role for job id 3.'),
(4, 7, 2, '2024-03-22 00:00:00', 'dear hr, i am very interested in the role for job id 7.'),
(5, 10, 2, '2024-02-17 00:00:00', 'dear hr, i am very interested in the role for job id 10.'),
(6, 9, 2, '2024-03-29 00:00:00', 'dear hr, i am very interested in the role for job id 9.'),
(7, 3, 3, '2024-05-15 00:00:00', 'dear hr, i am very interested in the role for job id 3.'),
(8, 4, 3, '2024-02-26 00:00:00', 'dear hr, i am very interested in the role for job id 4.'),
(9, 6, 3, '2024-03-17 00:00:00', 'dear hr, i am very interested in the role for job id 6.'),
(10, 1, 4, '2024-02-14 00:00:00', 'dear hr, i am very interested in the role for job id 1.');

--4
--check if  database  exists or not
if not exists (
    select name from sys.databases where name = 'careerhub'
)
begin
    create database careerhub;
    print 'database careerhub created.';
end
else
begin
    print 'database careerhub already exists.';
end;
go

--checking if companies table exists or not
if not exists (
    select * from information_schema.tables where table_name = 'companies'
)
begin
    print 'table companies created.';
end
else
begin
    print 'table companies already exists.';
end;
go

--checking if jobs table exists or not
if not exists (
    select * from information_schema.tables where table_name = 'jobs'
)
begin
    print 'table jobs created.';
end
else
begin
    print 'table jobs already exists.';
end;
go

--checking if applicants table exists or not
if not exists (
    select * from information_schema.tables where table_name = 'applicants'
)
begin
    print 'table applicants created.';
end
else
begin
    print 'table applicants already exists.';
end;
go
-- checking whether applications table exists
if not exists (
    select * from information_schema.tables where table_name = 'applications'
)
begin
    print 'table applications created.';
end
else
begin
    print 'table applications already exists.';
end;
go

--5
select j.jobtitle,count(a.applicationid) as application_count from jobs j
left join applications a on j.jobid = a.jobid
group by j.jobtitle;

--6
select j.jobtitle,c.companyname,c.location,j.salary from jobs j
join companies c on j.companyid=c.companyid
where j.salary between 600000.00 and 800000.00;

--7
select j.jobtitle,c.companyname,a.applicationdate from applications a
join jobs j on a.jobid = j.jobid
join companies c on j.companyid = c.companyid
where a.applicantid = 3;

--8
select avg(salary) avgsalary from jobs
where salary>0;

--9
select c.companyname, count(j.jobid) as job_count from companies c
join jobs j on c.companyid = j.companyid
group by c.companyname
having count(j.jobid) = (
    select top 1 count(jobid)
    from jobs
    group by companyid
    order by count(jobid) desc
);

--10
select distinct ap.fname, ap.lname, ap.experience from applications a
join applicants ap on a.applicantid = ap.applicantid
join jobs j on a.jobid = j.jobid
join companies c on j.companyid = c.companyid
where ap.experience >= 3 and c.location = 'chennai';

--11
select distinct jobtitle from jobs
where salary between 600000 and 800000;

--12
select j.jobtitle from jobs j
left join applications a on j.jobid=a.jobid
where a.applicationid is null;

--13
select ap.fname, ap.lname,c.companyname,j.jobtitle from applications a
join applicants ap on a.applicantid = ap.applicantid
join jobs j on a.jobid = j.jobid
join companies c on j.companyid = c.companyid;

--14
select c.companyname, count(j.jobid) as job_count from companies c
left join jobs j on j.companyid=c.companyid 
group by c.companyname;

--15
select ap.fname, ap.lname,c.companyname,j.jobtitle from applicants ap
left join applications a on ap.applicantid = a.applicantid
left join jobs j on a.jobid = j.jobid
left join companies c on j.companyid = c.companyid;

--16
select distinct c.companyname from jobs j
join companies c on j.companyid = c.companyid
where j.salary > (
    select avg(salary) from jobs
);

--17
alter table applicants
add city varchar(50),
state varchar(50);

update applicants set city = 'chennai', state = 'tamilnadu' where applicantid = 1;
update applicants set city = 'mumbai', state = 'maharashtra' where applicantid = 2;
update applicants set city = 'delhi', state = 'delhi' where applicantid = 3;
update applicants set city = 'hyderabad', state = 'telangana' where applicantid = 4;
update applicants set city = 'bengaluru', state = 'karnataka' where applicantid = 5;

select fname+' '+lname as name,city+' '+state as location from applicants;

--18
select jobtitle from jobs 
where jobtitle like '%developer%' or jobtitle like '%engineer%';

--19
select ap.fname,ap.lname,j.jobtitle from applicants ap
full outer join applications a on ap.applicantid=a.applicantid
full outer join jobs j on j.jobid=a.jobid;

--20
select ap.fname,ap.lname,c.companyname from applicants ap
cross join companies c 
where c.location = 'chennai' and ap.experience > 2;
