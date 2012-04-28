To build the BizCollge Data Access layer (DAL) and tests, you need the following:

-NHibernate 3.1.0 (projects are already setup and the assemblies are in the Libs folder of the BizCollege code repository)

-NUnit 2.5.1.0 (if you want to use a newer version, just update the NUnit assembly reference in the data access layer test project)

-MySQL Community Server v5.5.12 or greater (NHibernate is setup with MySQL drivers, i'll probably update it to use SQL Server express when i get a chance)

-MySQL Connector v6.3.6 (.Net database connector to MySQL - will only be used when doing database specific stuff, e.g. automate database creation for the unit tests Db)

Once you have the MySQL database setup, you need to update the hibernate.cfg.xml (NHibernate configuration) so that the connection string is update to point
to the right database you want to use, and using the right username and password for access to the Db.