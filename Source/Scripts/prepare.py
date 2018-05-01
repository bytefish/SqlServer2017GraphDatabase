from sqlalchemy import MetaData
from sqlalchemy import Table
from sqlalchemy import Column, Integer, String
import csv

metadata = MetaData()

airport = Table('Airport', metadata,
    id   = Column(Integer, primary_key=True)
    code = Column(String)
    city  = Column(Integer)
    state = Column(String)
    country = Column(String))

carrier = Table('Carrier', metadata)

with open('AIRPORT_ID.csv', newline='') as csvfile:
    reader = csv.DictReader(csvfile)
    for (idx, row) in reader:
        # Get Raw Data:
        code = row['Code']
        description = row['Description']
        
        # 
        # Build the Insert Statement:
        insert_statement = "INSERT INTO AIRPORT(ID, CODE, CITY, STATE, COUNTRY) VALUES ()".format(
        
        
     